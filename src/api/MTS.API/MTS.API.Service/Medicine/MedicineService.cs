using MTS.API.Service.Medicine.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MTS.API.Service.Medicine
{
    public class MedicineService : IMedicineService
    {
        private readonly IDataStore _dataStore;
        private readonly string _jsonFilePath;
        public MedicineService()
        {
            _jsonFilePath = Assembly.GetExecutingAssembly().Location.Replace("bin\\Debug\\netcoreapp3.1\\MTS.API.Service.dll", "MedicineDataStore.json");
            _dataStore = new DataStore(_jsonFilePath);
        }

        public Domain.Medicine GetMedicine(Guid id)
        {
            IDocumentCollection<Domain.Medicine> collection = GetMedicines();
            return collection.AsQueryable().Single(s => s.Id == id);
        }

        public IDocumentCollection<Domain.Medicine> GetMedicines()
        {
            return _dataStore.GetCollection<Domain.Medicine>();
        }

        public IEnumerable<MedicineViewModel> GetMedicinesViewModel()
        {
            IDocumentCollection<Domain.Medicine> collection = GetMedicines();
            return collection.AsQueryable().Select(model => DomainToViewModel(model));
        }

        public async Task<Guid> PostMedicineAsync(Domain.Medicine medicine)
        {
            medicine.Id = Guid.NewGuid();
            IDocumentCollection<Domain.Medicine> collection = GetMedicines();
            await collection.InsertOneAsync(medicine);
            return medicine.Id;
        }

        public async Task PutMedicineAsync(Domain.Medicine medicine)
        {
            IDocumentCollection<Domain.Medicine> collection = GetMedicines();
            await collection.UpdateOneAsync(medicine.Id, medicine);
        }

        public async Task DeleteMedicineAsync(Guid id)
        {
            IDocumentCollection<Domain.Medicine> collection = GetMedicines();
            await collection.DeleteOneAsync(id);
        }

        public MedicineViewModel DomainToViewModel(Domain.Medicine model)
        {
            MedicineViewModel viewModel = new MedicineViewModel
            {
                Brand = model.Brand,
                ExpiryDate = model.ExpiryDate,
                FullName = model.FullName,
                Id = model.Id,
                Price = model.Price,
                Quantity = model.Quantity,
                Notes = model.Notes
            };

            return SetBackgroundColor(viewModel);
        }

        private MedicineViewModel SetBackgroundColor(MedicineViewModel viewModel)
        {
            if (viewModel.Quantity < 10 && viewModel.ExpiryDate < DateTime.Now.AddDays(30))
            {
                viewModel.BackgroundColor = Color.Yellow.Name;
            }
            else if (viewModel.Quantity < 10)
            {
                viewModel.BackgroundColor = Color.Yellow.Name;
            }
            else if (viewModel.ExpiryDate < DateTime.Now.AddDays(30))
            {
                viewModel.BackgroundColor = Color.Red.Name;
            }

            return viewModel;
        }
    }
}
