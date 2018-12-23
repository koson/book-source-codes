using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using Microsoft.Data.Entity;

namespace Command.Core
{

    public class CreateEmailAccount :ICommand
    {
        private EmployeeManager manager;

        public CreateEmailAccount(EmployeeManager manager)
        {
            this.manager = manager;
        }

        public void Execute()
        {
            manager.CreateEmailAccount();
        }

        public void Undo()
        {
            manager.UndoCreateEmailAccount();
        }

    }
}
