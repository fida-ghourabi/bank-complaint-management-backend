using BankComplaintManagement.Application.DTOs.BankAccounts;
using BankComplaintManagement.Application.DTOs.Clients;
using BankComplaintManagement.Application.DTOs.Complaints;
using BankComplaintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.Mappings
{
    public static class ClientMapping
    {

        public static ClientDto ToDto(
            this Client client)
        {

            return new ClientDto
            {
                Id = client.Id,

                FirstName = client.FirstName,

                LastName = client.LastName,

                Email = client.Email,

                CustomerNumber = client.CustomerNumber,

                CIN = client.CIN,

                PhoneNumber = client.PhoneNumber,

                Status = client.Status.ToString()
            };

        }



        // ===================================================
        // Client List DTO
        // Administration
        // ===================================================


        public static ClientListDto ToListDto(
            this Client client)
        {

            return new ClientListDto
            {

                Id = client.Id,


                CustomerNumber =
                    client.CustomerNumber,


                CIN =
                    client.CIN,


                FirstName =
                    client.FirstName,


                LastName =
                    client.LastName,


                Email =
                    client.Email,


       


                Status =
                    client.Status,


                TotalComplaints =
                    client.Complaints.Count,


                OpenComplaints =
                    client.Complaints
                    .Count(c =>
                        c.Status ==
                        Domain.Enums.ComplaintStatus.Open)

            };

        }









        // ===================================================
        // Client Details DTO
        // Administration
        // Avec historique réclamation
        // ===================================================


        public static ClientDetailsDto ToDetailsDto(
            this Client client)
        {


            return new ClientDetailsDto
            {


                Id =
                    client.Id,


                CustomerNumber =
                    client.CustomerNumber,


                CIN =
                    client.CIN,


                FirstName =
                    client.FirstName,


                LastName =
                    client.LastName,


                Email =
                    client.Email,


                PhoneNumber =
                    client.PhoneNumber,


                Status =
                    client.Status,





                Complaints =
                    client.Complaints
                    .Select(c =>
                        new ComplaintHistoryDto
                        {

                            Id =
                                c.Id,


                            ReferenceNumber =
                                c.ReferenceNumber,


                            Subject =
                                c.Subject,


                            Category =
                                c.Category,


                            SubCategory =
                                c.SubCategory,


                            Status =
                                c.Status,


                            Priority =
                                c.Priority,


                            CreatedAt =
                                c.CreatedAt

                        })
                    .ToList()

            };

        }










        // ===================================================
        // Client Profile DTO
        // Client connecté
        // Avec comptes bancaires
        // ===================================================


        public static ClientProfileDto ToProfileDto(
            this Client client)
        {

            return new ClientProfileDto
            {


                Id =
                    client.Id,


                CustomerNumber =
                    client.CustomerNumber,


                FirstName =
                    client.FirstName,


                LastName =
                    client.LastName,


                Email =
                    client.Email,


                PhoneNumber =
                    client.PhoneNumber,





                Accounts =
                    client.BankAccounts
                    .Select(a =>
                        new BankAccountInfoDto
                        {

                            Id =
                                a.Id,


                            AccountNumber =
                                a.AccountNumber,


                            Type =
                                a.Type,


                            Balance =
                                a.Balance


                        })
                    .ToList()

            };

        }


    }

}