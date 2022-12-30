using ARMApocalypseSASAPI.Dtos;
using ARMApocalypseSASAPI.Interfaces;
using ARMApocalypseSASAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ARMApocalypseSASAPI.Tests
{
    public class CoreServiceTest
    {

        private ICoreService _coreService;
        public CoreServiceTest()
        {
            _coreService = new CoreServiceMock();
        }

        [Fact]
        public void Test_RegisterSurvivor_Returns_Right_Response()
        {
            //Arrange
            var newSurvivor = new RegisterSurvivorRequest()
            {
                //Id = Guid.NewGuid().ToString(),
                Name = "Survivor 005",
                Age = 26,
                Gender = "M",
                LastLocationLatitude = -123284233.233,
                LastLocationLongitude = 23632632732.1212
            };
            //Act
            var Result = _coreService.RegisterSurvivor(newSurvivor).Result;

            //Asssert
            Assert.IsType<GenericResponse<SurvivorResponse>>(Result);
        }

        [Fact]
        public void Test_Register_Actually_Pushed_Item_To_The_List()
        {
            //Arrange
            var newSurvivor = new RegisterSurvivorRequest()
            {
                //Id = Guid.NewGuid().ToString(),
                Name = "Survivor 005",
                Age = 26,
                Gender = "M",
                LastLocationLatitude = -123284233.233,
                LastLocationLongitude = 236326327.121
            };

            //Act
            var Result = _coreService.RegisterSurvivor(newSurvivor).Result;

            //Asssert
            //var Item = Assert.IsType<Response<Product>>(Result);
            var Item = Assert.IsType<GenericResponse<SurvivorResponse>>(Result);
            Assert.Equal(newSurvivor.Name, Item.Data.Name);
        }
        [Fact]
        public void Test_GetAllItems_Returns_Right_Response()
        {
            //Arrange


            //Act
            var Result = _coreService.GetAllItems().Result;

            //Assert
            Assert.IsType<GenericResponse<IEnumerable<ItemResponse>>>(Result);
        }
        [Fact]
        public void Test_GetAllItems_Returns_Correct_Number_Of_Items_In_Collection()
        {
            //Arrange

            //Act
            var Result = _coreService.GetAllItems().Result;

            //Assert
            var Items = Assert.IsType<GenericResponse<IEnumerable<ItemResponse>>>(Result);
            Assert.Equal(4, Items.Data.Count());
        }

    }
}
