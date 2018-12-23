using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Drawing;


namespace Decorator.Core
{
    public abstract class DecoratorBase : IPhoto
    {
        private IPhoto photo;

        public DecoratorBase(IPhoto photo)
        {
            this.photo = photo;
        }

        public virtual Bitmap GetPhoto()
        {
            return photo.GetPhoto();
        }
    }
}
