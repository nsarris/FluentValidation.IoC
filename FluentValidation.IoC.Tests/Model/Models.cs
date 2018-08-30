using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation.IoC.Tests.Model
{
    public class Customer 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string VatNumber { get; set; }
        public Address MainAddress { get; set; }
        public List<Address> OtherAddresses { get; set; }
    }

    public class Address 
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public Phone PrimaryPhone { get; set; }
        public List<Phone> OtherPhones { get; set; }
    }

    public enum PhoneKind
    {
        Home,
        Work,
        Mobile,
        Other
    }

    public class Phone
    {
        public Phone(string number, PhoneKind kind)
        {
            Number = number;
            Kind = kind;
        }

        public string Number { get; }
        public PhoneKind Kind { get; }
    }

    



    
    public class Invoice 
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateTime { get; set; }
        public List<InvoiceLine> InvoiceLines { get; set; }
    }

    
    public class InvoiceLine 
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}
