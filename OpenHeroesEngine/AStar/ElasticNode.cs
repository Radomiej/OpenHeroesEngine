using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenHeroesEngine.AStar
{
    class ElasticNode
    {
        private List<ElasticNode> neighboards = new List<ElasticNode>(4);

        public List<ElasticNode> GetNeighboards()
        {
            return neighboards;
        }

        public void AddNeighboard(ElasticNode neighboardToAdd)
        {
            neighboards.Add(neighboardToAdd);
        }
    }
}