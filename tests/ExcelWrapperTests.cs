using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests
{
    public class ExcelWrapperTests
    {
        [Fact]
        public void OpenReadNotExistingFileShouldBeThrowException()
        {
            Assert.Throws<OfficeWrapper.Exceptions.ExcelExceptions.InitializeException>(() => ExcelWrapper.OpenReadExcel("&^%&$%"));
        }
    }
}
