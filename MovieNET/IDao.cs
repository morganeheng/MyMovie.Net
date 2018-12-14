using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieNET
{
    interface IDao<TEntity>
    {
        int Create(TEntity entity);
        void Delete(TEntity entity);
        void Modify(TEntity entity);
        TEntity SelectOne(int id);
        List<TEntity> SelectAll();
    }
}
