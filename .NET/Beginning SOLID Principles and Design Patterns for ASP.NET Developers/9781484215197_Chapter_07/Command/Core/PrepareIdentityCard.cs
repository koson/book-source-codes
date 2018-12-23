using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Command.Core
{
    public class PrepareIdentityCard : ICommand
    {
        private EmployeeManager manager;

        public PrepareIdentityCard(EmployeeManager manager)
        {
            this.manager = manager;
        }

        public void Execute()
        {
            manager.PrepareIdentityCard();
        }

        public void Undo()
        {
            manager.UndoPrepareIdentityCard();
        }
    }
}
