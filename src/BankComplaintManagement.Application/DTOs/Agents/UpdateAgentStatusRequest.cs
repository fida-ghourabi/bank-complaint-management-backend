using BankComplaintManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.Agents
{
    public class UpdateAgentStatusRequest
    {

        public AgentStatus Status { get; set; }

    }
}
