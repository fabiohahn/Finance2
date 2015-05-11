using System.Collections;

namespace FinanceMvc.Repositories.Memory
{
    public abstract class RepositoryBase<TObject> where TObject : Finance.Entity, new()
    {
        protected readonly Hashtable Data = new Hashtable();

        public virtual TObject Get(int id)
        {
            return (TObject)Data[id];
        }

        public virtual void Add(TObject t)
        {
            t.Id = Data.Count + 1;
            Data.Add(t.Id, t);            
        }
        
        public virtual void Update(TObject t, int key)
        {
            Data.Add(t.Id, t);
        }

        public virtual void Remove(TObject t)
        {
            Data.Remove(t.Id);
        }

        public void Dispose()
        {
        
        }
    }   
}