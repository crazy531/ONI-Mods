using PeterHan.PLib.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crazyxyr_mod
{
    public abstract class SingletonOptions<T> where T : class, new()
    {
        // Token: 0x17000001 RID: 1
        // (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        // (set) Token: 0x06000002 RID: 2 RVA: 0x0000209C File Offset: 0x0000029C
        public static T Instance
        {
            get
            {
                bool flag = SingletonOptions<T>.instance == null;
                if (flag)
                {
                    T t;
                    bool flag2 = (t = POptions.ReadSettings<T>()) == null;
                    if (flag2)
                    {
                        t = Activator.CreateInstance<T>();
                    }
                    SingletonOptions<T>.instance = t;
                }
                return SingletonOptions<T>.instance;
            }
            protected set
            {
                bool flag = value != null;
                if (flag)
                {
                    SingletonOptions<T>.instance = value;
                }
            }
        }

        // Token: 0x04000001 RID: 1
        protected static T instance;
    }
}
