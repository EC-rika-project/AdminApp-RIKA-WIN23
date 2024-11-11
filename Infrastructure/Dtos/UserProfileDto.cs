using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dtos
{
    public class UserProfileDto
    {

        public string? Id { get; set; }
        public string? UserName { get; set; }
        public List<string>? UserRoles { get; set; }
        public bool IsExternalAccount { get; set; }
        public ProfileDto? Profile { get; set; }
        public AddressDto? Address { get; set; }
        public WishListDto? WishList { get; set; }
        public List<ShoppingCartDto>? ShoppingCarts { get; set; }


    }

    public class ProfileDto
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? Gender { get; set; }  // Can be null
        public int Age { get; set; }
    }

    public class AddressDto
    {
        public string? AddressLine { get; set; }  // Can be null
        public string? ApartmentNumber { get; set; }  // Can be null
        public int PostalCode { get; set; }
        public string? City { get; set; }  // Can be null
        public string? Country { get; set; }  // Can be null
    }

    public class WishListDto
    {
        public List<Guid>? ProductIds { get; set; }  // List of product IDs
    }

    public class ShoppingCartDto
    {
        public Guid Id { get; set; }
        public List<ProductDto>? Products { get; set; }  // List of ProductDto objects
        public decimal TotalPrice { get; set; }
    }

    public class ProductDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
    }
}
