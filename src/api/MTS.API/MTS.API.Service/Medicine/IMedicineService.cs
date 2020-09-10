using MTS.API.Service.Medicine.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MTS.API.Service.Medicine
{
    public interface IMedicineService
    {
        IDocumentCollection<Domain.Medicine> GetMedicines();
        IEnumerable<MedicineViewModel> GetMedicinesViewModel();
        Domain.Medicine GetMedicine(int id);
        Task PutMedicineAsync(Domain.Medicine medicine);
        Task<int> PostMedicineAsync(Domain.Medicine medicine);
        Task DeleteMedicineAsync(int id);
    }
}
