using SmartIT.Module.Model;
using System.Collections.Generic;

namespace SmartIT.Module
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAll();
        Company Get(int id);
        Company Add(Company item);
        void Remove(int id);
        void Import();
        bool Update(Company item);
    }

}