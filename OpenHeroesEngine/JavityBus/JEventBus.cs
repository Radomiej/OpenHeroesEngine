using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Radomiej.JavityBus.Utils;

namespace Radomiej.JavityBus
{
    public class JEventBus
    {
        private static Dictionary<string, JEventBus> eventBuses = new Dictionary<string, JEventBus>();

        public static JEventBus GetEventBusByName(string eventBusName)
        {
            return eventBuses[eventBusName];
        }

        private static JEventBus defaultInstance;

        public static JEventBus GetDefault()
        {
            if (defaultInstance == null)
            {
                defaultInstance = new JEventBus();
                defaultInstance.Awake();
            }
            return defaultInstance;
        }


        private Dictionary<Type, SortedList<PriorityDelegate>> subscribtions = new Dictionary<Type, SortedList<PriorityDelegate>>();
        private Dictionary<object, List<PriorityDelegate>> recivers = new Dictionary<object, List<PriorityDelegate>>();

        private string _name;

        void Awake(string eventBusName = "default")
        {
            _name = eventBusName;
            
            if (eventBuses.ContainsKey(_name))
            {
                // Debug.LogWarning("Overriding exist EventBus with name: " + _name);
            }

            eventBuses.Add(_name, this);
        }

        void OnDestroy()
        {
            if (defaultInstance == this)
            {
                //defaultInstance = null;
            }

            eventBuses.Remove(_name);
        }

        public void Post(object eventObject)
        {
            if (!subscribtions.ContainsKey(eventObject.GetType()))
            {
                #if DEBUG
                // Debug.LogWarning("Receiver for event: " + eventObject.GetType() + " not exist");
                #endif
                return;
            }

            SortedList<PriorityDelegate> receiverDelegates = subscribtions[eventObject.GetType()];
            for (int i = 0; i < receiverDelegates.Count; i++)
            {
                Delegate delegateToInvoke = receiverDelegates[i].Handler;
                delegateToInvoke?.DynamicInvoke(eventObject);
            }
        }

        public delegate void RawSubscribe(object incomingEvent);
        public void Register(object objectToRegister, IJEventSubscriberRaw subscriberRaw)
        {
            RawSubscribe singleDelegate = subscriberRaw.SubscribeRaw;
            int priority = subscriberRaw.GetPriority();
           
            PriorityDelegate priorityDelegate = AddSubscription(subscriberRaw.GetEventType(), singleDelegate, priority);
            
            if (!recivers.ContainsKey(objectToRegister))
            {
                var delegates = new List<PriorityDelegate>();
                recivers.Add(objectToRegister, delegates);
            } 
            
            recivers[objectToRegister].Add(priorityDelegate);
        }

        public void Register(object objectToRegister)
        {
            recivers.Add(objectToRegister, new List<PriorityDelegate>());

            MethodInfo[] methods = objectToRegister.GetType().GetMethods(BindingFlags.NonPublic |
                                                                         BindingFlags.Instance | BindingFlags.Public);
            for (int m = 0; m < methods.Length; m++)
            {
                object[] attributes = methods[m].GetCustomAttributes(true);
                for (int i = 0; i < attributes.Length; i++)
                {
                    if (attributes[i] is Subscribe subscribe)
                    {
                        MethodInfo method = methods[m];
                        if (method.GetParameters().Length != 1) continue;

                        ParameterInfo firstArgument = method.GetParameters()[0];
                        List<Type> args = new List<Type>(
                            method.GetParameters().Select(p => p.ParameterType));
                        Type delegateType;
                        if (method.ReturnType == typeof(void))
                        {
                            delegateType = Expression.GetActionType(args.ToArray());
                        }
                        else
                        {
                            args.Add(method.ReturnType);
                            delegateType = Expression.GetFuncType(args.ToArray());
                        }

                        Delegate d = Delegate.CreateDelegate(delegateType, objectToRegister, method.Name);
                        PriorityDelegate priorityDelegate = AddSubscription(firstArgument.ParameterType, d, subscribe.priority);
                        recivers[objectToRegister].Add(priorityDelegate);
                    }
                }
            }
        }

        private PriorityDelegate AddSubscription(Type eventType, Delegate d, int priority = 0)
        {
            PriorityDelegate priorityDelegate = new PriorityDelegate(priority, d);
            if (!subscribtions.ContainsKey(eventType))
            {
                // subscribtions.Add(eventType, new List<PriorityDelegate>());
                subscribtions.Add(eventType, new SortedList<PriorityDelegate>());
            }
            

            subscribtions[eventType].Add(priorityDelegate);
            return priorityDelegate;
        }

        public void Unregister(object objectToUnregister)
        {
            if (!recivers.ContainsKey(objectToUnregister)) return;

            MethodInfo[] methods = objectToUnregister.GetType().GetMethods();
            for (int m = 0; m < methods.Length; m++)
            {
                object[] attributes = methods[m].GetCustomAttributes(true);
                for (int i = 0; i < attributes.Length; i++)
                {
                    if (attributes[i] is Subscribe)
                    {
                        MethodInfo method = methods[m];
                        if (method.GetParameters().Length != 1) continue;

                        ParameterInfo firstArgument = method.GetParameters()[0];
                        foreach (PriorityDelegate priorityDelegate in recivers[objectToUnregister])
                        {
                            subscribtions[firstArgument.ParameterType].Remove(priorityDelegate);
                        }
                    }
                }
            }

            recivers.Remove(objectToUnregister);
        }

        public void ClearAll()
        {
            subscribtions.Clear();
            recivers.Clear();
        }
    }
}