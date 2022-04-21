using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.MapGet("/user", () => "Enzo Mascarenhas");

app.MapPost("/saveproduct", (Product product) =>  {
      ProductRepository.add(product);
});

app.MapGet("/getproduct", ([FromQuery] string dateStart,[FromQuery] string dateEnd)=>{
    return dateStart + " - "  + dateEnd;   
});
app.MapGet("/getproduct/{code}", ([FromRoute] string code ) => {
   var product = ProductRepository.Getby(code);
   return product;
});

app.MapPut("/editproduct", (Product product) => {
    var productSaved = ProductRepository.Getby(product.Code);
    productSaved.Name = product.Name;
});
app.MapDelete("/deleteproduct/{code}", ([FromRoute] string code ) => {
   var product = ProductRepository.Getby(code);
   ProductRepository.Remove(product); 

   });

app.MapGet("/getproductbyheader", (HttpRequest request) => {
    return request.Headers["product-code"].ToString();

    });

app.Run();

public static class ProductRepository {
      
    public static List <Product> Products {get; set;}
     public static void add (Product product) {
     if (Products == null)
         Products = new List<Product>();

         Products.Add (product);
        }
       public static Product Getby(string code){
           return Products.FirstOrDefault(p => p.Code == code);
} 
        public static void Remove (Product product) {
            Products.Remove(product);
          
       }
}
public class Product {

    public string Code { get; set;}
    public string Name { get; set;}
}




