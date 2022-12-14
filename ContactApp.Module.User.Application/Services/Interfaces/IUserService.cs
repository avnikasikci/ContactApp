using ContactApp.Core.Application.SharedModels;
using ContactApp.Module.User.Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Module.User.Application.Services.Interfaces
{
    public interface IUserService
    {
        IQueryable<EntityUser> GetAll();
        EntityUser Save(EntityUser entityPerson);
        EntityUser Add(EntityUser entityPerson);
        EntityUser Update(EntityUser entityPerson);
        EntityUser SelectById(int id);
        CustomerReport GetCustomerReport(string ReportName,DateTime AddedOnDate);
    }
}
