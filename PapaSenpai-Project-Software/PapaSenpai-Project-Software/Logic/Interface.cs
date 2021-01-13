using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaSenpai_Project_Software.Logic
{
    public interface Interface
    {
        public void Create(string[] bindings);

        public void Update(string[] bindings);

        public void Delete(string[] bindings);
    }
}
