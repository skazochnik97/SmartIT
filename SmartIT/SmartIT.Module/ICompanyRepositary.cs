using SmartIT.Module.Model;
using System.Collections.Generic;

namespace SmartIT.Module
{
    public interface ICompanyRepositary
    {
        IEnumerable<Company> GetAll();
        Company Get(int id);
        Company Add(Company item);
        void Remove(int id);
        bool Update(Company item);
    }

}