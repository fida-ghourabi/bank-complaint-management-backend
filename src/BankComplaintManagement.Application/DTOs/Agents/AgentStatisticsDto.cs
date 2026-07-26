using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.Agents
{
    public class AgentStatisticsDto
    {

        public int TotalAgents { get; set; }


        public int ActiveAgents { get; set; }


        public int InactiveAgents { get; set; }


        public double AverageWorkload { get; set; }

    }
}
