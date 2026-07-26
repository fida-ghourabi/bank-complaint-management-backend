using BankComplaintManagement.Application.DTOs.Agents;
using BankComplaintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Mappings
{
    public static class AgentMapping
    {


        public static AgentDetailsDto ToDetailsDto(
            this Agent agent)
        {

            return new AgentDetailsDto
            {

                Id = agent.Id,

                Matricule = agent.Matricule,

                FirstName = agent.FirstName,

                LastName = agent.LastName,

                Email = agent.Email,

                PhoneNumber = agent.PhoneNumber,

                Status = agent.Status

            };

        }





        public static AgentProfileDto ToProfileDto(
            this Agent agent,
            int processed,
            int inProgress,
            int overdue,
            double rate)
        {

            return new AgentProfileDto
            {

                Id = agent.Id,

                Matricule = agent.Matricule,

                FirstName = agent.FirstName,

                LastName = agent.LastName,

                Email = agent.Email,

                PhoneNumber = agent.PhoneNumber,

                Position = agent.Position,

                Service = agent.Service,

                CreatedAt = agent.CreatedAt,

                ProcessedComplaints = processed,

                InProgressComplaints = inProgress,

                OverdueComplaints = overdue,

                ResolutionRate = rate

            };

        }


        public static AgentDto ToDto(
            this Agent agent)
        {

            return new AgentDto
            {

                Id = agent.Id,

                Matricule = agent.Matricule,

                FirstName = agent.FirstName,

                LastName = agent.LastName,

                Email = agent.Email,

                PhoneNumber = agent.PhoneNumber,

                Status = agent.Status

            };

        }





        public static AgentDto ToDto(
            this Agent agent,
            int assignedComplaints,
            int inProgressComplaints,
            double resolutionRate)
        {

            return new AgentDto
            {

                Id = agent.Id,


                Matricule = agent.Matricule,


                FirstName = agent.FirstName,


                LastName = agent.LastName,


                Email = agent.Email,


                PhoneNumber = agent.PhoneNumber,


                Status = agent.Status,


                AssignedComplaints =
                    assignedComplaints,


                InProgressComplaints =
                    inProgressComplaints,


                ResolutionRate =
                    resolutionRate

            };

        }






        public static CreatedAgentDto ToCreatedDto(
            this Agent agent,
            string temporaryPassword)
        {

            return new CreatedAgentDto
            {

                Id = agent.Id,


                Matricule = agent.Matricule,


                FirstName = agent.FirstName,


                LastName = agent.LastName,


                Email = agent.Email,


                PhoneNumber = agent.PhoneNumber,


                TemporaryPassword =
                    temporaryPassword

            };

        }



    }
}
