using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P07_Findexium_Unit_Tests.Helpers
{
    public class MockDbSet<T> : Mock<DbSet<T>> where T : class
    {
        public MockDbSet(IQueryable<T> data)
        {
            this.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            this.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            this.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            this.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        }
    }
}
