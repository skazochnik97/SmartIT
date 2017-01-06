using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB;
using LinqToDB.Common;
using LinqToDB.Data;
using SmartIT.Module.Model;

namespace SmartIT.Module
{
    public class CompanyRepository: ICompanyRepositary
    {
        private List<Company> products = new List<Company>();
        private SmartITDataBase db;
        public CompanyRepository(SmartITDataBase database)
        {
            db = database;
        }

        public void Import()
        {
            Add(new Company { Name = "ООО \"НЛМК - Информационные технологии\"", ShortName = "НЛМК-ИТ", DateCreate = DateTime.Parse("01.01.2013") });
            Add(new Company { Name = "ПАО НЛМК", ShortName = "ПАО НЛМК", DateCreate = DateTime.Parse("01.01.1934") });
            Add(new Company { Name = "НМЛК-Учетный центр", ShortName = "НМЛК-Учетный центр", DateCreate = DateTime.Parse("01.01.2014") });
            Add(new Company { Name = "НМЛК-калуга", ShortName = "НМЛК-калуга", DateCreate = DateTime.Parse("06.10.2012") });
        }

        public IEnumerable<Company> GetAll()
        {
                return db.Company;
        }

        public Company Get(int id)
        {
                var query = from company in db.Company
                            where company.Id == id
                            orderby company.Name descending
                            select company;
                return query.First<Company>();
        }


        public Company Add(Company item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

                var query = from company in db.Company
                            where company.Name == item.Name
                            select company;

                if (query.Count() == 0)
                    db.Insert(item);
                return item;
        }

        public void Remove(int id)
        {

                db.Company
                    .Where(p => p.Id == id)
                    .Delete();
        }

        public bool Update(Company item)
        {

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

                db.Company
                      .Where(p => p.Id == item.Id)
                      .Set(p => p.Name, item.Name)
                      .Set(p => p.DateCancel, item.DateCancel)
                       .Set(p => p.DateCreate, item.DateCreate)
                           .Set(p => p.ShortName, item.ShortName)
                      .Update();
            return true;
        }
    }

}
