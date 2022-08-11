using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ordering.Domain.Entities.Base
{
    public abstract class Entity : IEntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; protected set; }

        public Entity Clone()//başka yerden çağırdığında çoğaltmak için kullanıyoruz. Order.Clone() ile çağaltabiliyoruz
        {
            return (Entity)this.MemberwiseClone();
        }
    }
}
