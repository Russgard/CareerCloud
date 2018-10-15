using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDataAccessLayer<T>
    {

        void Verify(T[] pocos);


        T Get(Guid id);


        List<T> GetAll();


        void Add(T[] pocos);


        void Update(T[] pocos);


        void Delete(T[] pocos);
    }
}
