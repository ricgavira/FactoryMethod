using DesignPattern.Application.Models;
using DesignPattern.Infrastructure.Payments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesignPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactoryMethodController : ControllerBase
    {
        private readonly IPaymentServiceFactory _paymentServiceFactory;

        public FactoryMethodController(IPaymentServiceFactory paymentServiceFactory)
        {
            _paymentServiceFactory = paymentServiceFactory;
        }

        [HttpPost]
        public IActionResult Post(OrderInputModel model)
        {
            ///// Problema apresentado - o código sempre vai crecendo muito conforme vamos
            /// tendo a necessidade de mais opçoes de pagamento.
            /// 
            //switch (model.PaymentInfo.PaymentMethod) 
            //{
            //    case PaymentMethod.CreditCard:
            //        //codigo para processar pagamento com cartao
            //        break;
            //    case PaymentMethod.PaymentSlip:
            //        //codigo para gerar o boleto
            //        break;
            //    default:
            //        return BadRequest("Meio de pagamento não suportado.");
            //}

            ///// Implementando Design Pattern Factory Method
            var paymentService = _paymentServiceFactory.GetService(model.PaymentInfo.PaymentMethod);

            paymentService.Process(model);

            return NoContent();
        }
    }
}