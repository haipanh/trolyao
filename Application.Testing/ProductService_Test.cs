using Application.Domain;
using Application.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solar.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Testing
{
    [TestClass]
    public class ProductService_Test : TestBase
    {
        private static ProductService _service = new ProductService();

        private void CreateProduct_Sample(string sku)
        {
            using (var _repository = DomainRepository.Open())
            {
                _repository.Add(new Product
                {
                    Sku = sku
                });
            }
        }

        [TestMethod]
        public void GetProductBySku_AreEqualSku_ReturnProductDto()
        {
            var _sku = "Sku_Test";

            CreateProduct_Sample(_sku);

            var _product = _service.GetProductBySku(_sku);

            Assert.IsNotNull(_product);
            Assert.AreEqual(_product.Sku, _sku);
        }
    }
}
