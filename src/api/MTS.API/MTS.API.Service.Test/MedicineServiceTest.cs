using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTS.API.Service.Medicine;
using MTS.API.Service.Medicine.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTS.API.Service.Test
{
    [TestClass]
    public class MedicineServiceTest
    {
        MedicineService _medicineService;
        public MedicineServiceTest()
        {
            _medicineService = new MedicineService();
        }

        [TestMethod]
        public void GetMedicines_ValidInput_ShouldReturnMedicines()
        {
            // Arrange => Act
            IDocumentCollection<Domain.Medicine> collection = _medicineService.GetMedicines();
            // Assert
            Assert.IsNotNull(collection);
            Assert.AreNotEqual(0, collection.Count);
        }


        [TestMethod]
        public void GetMedicines_ValidInput_ShouldReturnMedicinesViewModel()
        {
            // Arrange => Act
            IEnumerable<MedicineViewModel> collection = _medicineService.GetMedicinesViewModel();
            // Assert
            Assert.IsNotNull(collection);
            Assert.AreNotEqual(0, collection.Count());
        }

        [TestMethod]
        public async Task GetMedicine_ValidInputint_ShouldReturnMedicines()
        {
            // Arrange 
            Domain.Medicine domain = new Domain.Medicine()
            {
                Brand = "Unit Test Brand",
                ExpiryDate = DateTime.Now.AddDays(90),
                FullName = "Unit Test Name",
                Notes = "Unit Test Notes",
                Price = 100,
                Quantity = 200
            };

            // Act
            int id = await _medicineService.PostMedicineAsync(domain);
            // Act
            Domain.Medicine medicine = _medicineService.GetMedicine(id);
            // Assert
            Assert.IsNotNull(medicine);
            Assert.AreEqual(id, medicine.Id);
        }

        [TestMethod]
        public async Task PostMedicine_ValidInputint_ShouldReturnMedicines()
        {
            // Arrange 
            Domain.Medicine domain = new Domain.Medicine()
            {
                Brand = "Unit Test Brand",
                ExpiryDate = DateTime.Now.AddDays(90),
                FullName = "Unit Test Name",
                Notes = "Unit Test Notes",
                Price = 100,
                Quantity = 200
            };

            // Act
            int medicineId = await _medicineService.PostMedicineAsync(domain);
            // Assert
            Assert.IsNotNull(medicineId);
            Assert.AreEqual(medicineId, domain.Id);
        }

        [TestMethod]
        public async Task PutMedicine_ValidInputint_ShouldReturnMedicines()
        {
            // Arrange 
            Domain.Medicine domain = new Domain.Medicine()
            {
                Brand = "Unit Test Brand",
                ExpiryDate = DateTime.Now.AddDays(90),
                FullName = "Unit Test Name",
                Notes = "Unit Test Notes",
                Price = 200,
                Quantity = 300,
                Id = 1
            };

            // Act => Assert
            await _medicineService.PutMedicineAsync(domain);
        }

        [TestMethod]
        public async Task DeleteMedicine_ValidInputint_ShouldReturnMedicines()
        {
            // Arrange => Act => Assert
            await _medicineService.DeleteMedicineAsync(1);
        }
    }
}
