﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using PROG_Part_2.Controllers;
using PROG_Part_2.Models;
using PROG_Part_2.Services;

namespace TestPROG
{
    public class UnitTest2
    {
        [Fact]
        public void ApproveClaim_ValidId_UpdatesClaimStatus()
        {
            var fileServiceMock = new Mock<AzureFileShareService>("connectionString", "fileShareName"); // (RuchiG, 2023)
            var controller = new AdminController(fileServiceMock.Object);
            var claim = new Claims
            {
                ClaimId = 1,
                Name = "Zao",
                HoursWorked = 40,
                HourlyRate = 200,
                Status = "Pending"
            };

            ClaimsController._claimsList.Add(claim);

            var result = controller.ApproveClaim(1) as RedirectToActionResult; // (RuchiG, 2023)
            Assert.NotNull(result);
            Assert.Equal("PendingClaims", result.ActionName);
            Assert.Equal("Approved", claim.Status);
        }
    }
}
