using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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


        private Dictionary<Type, List<Delegate>> subscribtions = new Dictionary<Type, List<Delegate>>();
        private Dictionary<object, List<Delegate>> recivers = new Dictionary<object, List<Delegate>>();

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

            List<Delegate> receiverDelegates = subscribtions[eventObject.GetType()];
            for (int i = 0; i < receiverDelegates.Count; i++)
            {
                Delegate delegateToInvoke = receiverDelegates[i];
                delegateToInvoke?.DynamicInvoke(eventObject);
            }
        }

        public void Register(object objectToRegister)
        {
            recivers.Add(objectToRegister, new List<Delegate>());

            MethodInfo[] methods = objectToRegister.GetType().GetMethods(BindingFlags.NonPublic |
                                                                         BindingFlags.Instance | BindingFlags.Public);
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
                        if (!subscribtions.ContainsKey(firstArgument.ParameterType))
                        {
                            subscribtions.Add(firstArgument.ParameterType, new List<Delegate>());
                        }

                        subscribtions[firstArgument.ParameterType].Add(d);
                        recivers[objectToRegister].Add(d);
                    }
                }
            }
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
                        foreach (Delegate d in recivers[objectToUnregister])
                        {
                            subscribtions[firstArgument.ParameterType].Remove(d);
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