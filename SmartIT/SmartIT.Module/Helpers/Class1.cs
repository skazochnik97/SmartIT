using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartIT.Module.Helpers
{
    public class CmdletCacheHelper
    {
     /*   public static T GetCachedClientObject<T>(ServerConnection serverConn, Guid objId, CarmineObjectType objType, OnBehalfOf obo) where T : ClientObject
        {
            if (serverConn == null)
                throw new CarmineException((ErrorInfo)new Errors.InvalidArgumentFault("Not Available", "Need a valid server connection reference to retrieve cached client object", ""));
            T obj = serverConn.ObjectCache.GetCachedObject<T>(objId, obo);
            if ((object)obj == null || !obj.IsFullyCached)
            {
                foreach (ClientObject waitForObject in serverConn.ObjectCache.WaitForObjects(serverConn.Channel.GetLibraryObjectByIDWithChildren(objId, objType, obo), obo))
                {
                    if (waitForObject.ID == objId)
                    {
                        obj = (T)waitForObject;
                        break;
                    }
                }
                if ((object)obj == null || !obj.IsFullyCached)
                    obj = default(T);
            }
            return obj;
        }
        */
    }
}
