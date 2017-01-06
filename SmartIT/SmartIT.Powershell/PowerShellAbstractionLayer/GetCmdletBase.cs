using SmartIT.Module.Model;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace SmartIT.Module.PowerShellAbstractionLayer
{
    /* базовый командлет для вызова GET, по умолчанию реализует запрос всех объектов. Выборка конкретных объектов по параметрам реализуется внутри конечных командлетов */

    public abstract class GetCmdletBase<ObjectType>:SmartIT.Module.PowerShellAbstractionLayer.ConnectionCmdlet where ObjectType : BaseObject


    {
        protected const string allParameterSet = "All";
        protected const string idParameterSet = "ID";

        [Parameter(Mandatory = false, ParameterSetName = "All", ValueFromPipeline = false)]
        public SwitchParameter All;

        [Parameter(Mandatory = false, ParameterSetName = "ID", ValueFromPipeline = false)]
        [Alias("Oid")]
        public Guid? ID;

        public GetCmdletBase()
          : base()
        {
        }

        /// <summary>
        /// Получение списка базового объекта
        /// </summary>
        /// <param name="objType"></param>
        /// <returns></returns>
        protected virtual List<T> ProcessBase<T>()
        {
            List<T> businessObjectList =new List<T>() ;

         /*  UnitOfWork uow = new UnitOfWork(this.serverConnection.Client);
            XPCollection<T> collBaseBusinessObject;

                if (this.ID.HasValue)
                {
                // получаем объекты по указанному ID
                // передаём в обработку <тип результат> соединение с сервером, идентификатор
                collBaseBusinessObject = new XPCollection<T>(uow, CriteriaOperator.Parse("Oid=?", this.ID));
                    if (collBaseBusinessObject.Count > 0)
                        businessObjectList = collBaseBusinessObject.ToList<T>();
                }
                else
                {
                // получаем все объекты
                collBaseBusinessObject = new XPCollection<T>(uow);
                          if (collBaseBusinessObject.Count > 0)
                            businessObjectList = collBaseBusinessObject.ToList<T>();
                }*/
            return businessObjectList;
        }

        /// <summary>
        /// Получение списка базового объекта на основе фильтра
        /// </summary>
        /// <param name="objType"></param>
        /// <returns></returns>
        protected virtual List<T> ProcessBase<T>(string param,string value)
        {
            List<T> businessObjectList = new List<T>();

         /*   UnitOfWork uow = new UnitOfWork(this.serverConnection.Client);
            XPCollection<T> collBaseBusinessObject;

          
                // получаем объекты по указанному ID
                // передаём в обработку <тип результат> соединение с сервером, идентификатор
                collBaseBusinessObject = new XPCollection<T>(uow, CriteriaOperator.Parse($"{param}=?", value));
                if (collBaseBusinessObject.Count > 0)
                    businessObjectList = collBaseBusinessObject.ToList<T>();
          */
            return businessObjectList;
        }
        /*        protected List<ResultType> FilterResults<ResultType>(string name, List<ResultType> unfilteredResults, bool ignoreCase)// where ResultType : BaseBusinessObject
                {
                    StringComparison comparisonRule = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
                    return unfilteredResults.FindAll((Predicate<ResultType>)(x => string.Equals(x.Name, name, comparisonRule)));
                }
                */
        /*  public static MalCommand<ObjectType> CreateCommand(ServerConnection connection)
          {
              return new MalCommand<ObjectType>("Get-" + LibClientUtil.GetNounByObjectType(typeof(ObjectType)), new CommandParameter[1]
              {
          new CommandParameter("VMMServer", (object) connection)
              });
          }

          public static MalCommand<ObjectType> CreateCommand(ServerConnection connection, Guid ID)
          {
              MalCommand<ObjectType> command = GetCmdletBase<ObjectType>.CreateCommand(connection);
              command.AddCommandParameter(new CommandParameter("ID", (object)ID));
              return command;
          }*/
    }
}
