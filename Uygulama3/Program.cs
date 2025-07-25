using System.Reflection;
using System.Text;
using System.Text.Json;
using Uygulama3.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Uygulama2
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1-GET");
                Console.WriteLine("2-POST");
                Console.WriteLine("3-PUT");
                Console.WriteLine("4-DELETE");
                Console.WriteLine("Seçim (1/2/3/4):");
                var secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        Console.Write("Get (pokemon/food/owner/country/review/reviewer/category/colour): ");
                        string type = Console.ReadLine();
                        await GetData(type);
                        break;

                    case "2":
                        Console.Write("Post (pokemon/food/owner/country/review/reviewer/category/colour): ");
                        string postType = Console.ReadLine();
                        await PostData(postType);
                        break;

                    case "3":
                        Console.Write("Put (pokemon/food/owner/country/review/reviewer/category/colour): ");
                        string putType = Console.ReadLine();
                        await PutData(putType);
                        break;

                    case "4":
                        Console.Write("Delete (pokemon/food/owner/country/review/reviewer/category/colour): ");
                        string deleteType = Console.ReadLine();
                        await DeleteData(deleteType);
                        break;


                    default:
                        Console.WriteLine("Geçersiz Seçim...");
                        break;
                }

                Console.WriteLine("Devam etmek için bir tuşa basın...");
                Console.ReadKey();
            }
        }
        static async Task GetData(string type)
        {
            string baseUrl = "https://localhost:7170/api/";
            string fullUrl = baseUrl + type;

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            using var client = new HttpClient(handler);

            try
            {
                HttpResponseMessage response = await client.GetAsync(fullUrl);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();

                    switch (type)
                    {
                        case "pokemon":
                            var pokemons = JsonConvert.DeserializeObject<List<PokemonDto>>(responseBody);
                            foreach (var p in pokemons)
                                Console.WriteLine($"ID: {p.Id}, Name: {p.Name}, BirthDate: {p.BirthDate}");
                            break;

                        case "owner":
                            var owners = JsonConvert.DeserializeObject<List<OwnerDto>>(responseBody);
                            foreach (var o in owners)
                                Console.WriteLine($"ID: {o.Id}, FirstName: {o.FirstName}, LastName: {o.LastName},Gym:{o.Gym}");
                            break;

                        case "food":
                            var foods = JsonConvert.DeserializeObject<List<FoodDto>>(responseBody);
                            foreach (var f in foods)
                                Console.WriteLine($"ID: {f.Id}, Name: {f.Name}, Id: {f.PokemonId}");
                            break;

                        case "colour":
                            var colours = JsonConvert.DeserializeObject<List<ColourDto>>(responseBody);
                            foreach (var col in colours)
                                Console.WriteLine($"ID: {col.Id}, Name: {col.Name}, Id: {col.PokemonId}");
                            break;

                        case "country":
                            var countries = JsonConvert.DeserializeObject<List<CountryDto>>(responseBody);
                            foreach (var c in countries)
                                Console.WriteLine($"ID: {c.Id}, Name: {c.Name}");
                            break;

                        case "category":
                            var categories = JsonConvert.DeserializeObject<List<CategoryDto>>(responseBody);
                            foreach (var cat in categories)
                                Console.WriteLine($"ID: {cat.Id}, Name: {cat.Name}");
                            break;

                        case "reviewer":
                            var reviewers = JsonConvert.DeserializeObject<List<ReviewerDto>>(responseBody);
                            foreach (var rev in reviewers)
                                Console.WriteLine($"ID: {rev.Id}, FirstName: {rev.FirstName}, LastName: {rev.LastName}");
                            break;

                        case "review":
                            var reviews = JsonConvert.DeserializeObject<List<ReviewDto>>(responseBody);
                            foreach (var r in reviews)
                                Console.WriteLine($"ID: {r.Id}, Title: {r.Title}, Text:{r.Text}, Rating: {r.Rating}");
                            break;

                    }
                }
                else
                {
                    Console.WriteLine($"Başarısız API isteği: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static async Task PutData(string type)
        {
            switch (type)
            {
                case "review":
                    await PutReview();
                    break;
                case "pokemon":
                    await PutPokemon();
                    break;
                case "food":
                    await PutFood();
                    break;
                case "colour":
                    await PutColour();
                    break;
                case "owner":
                    await PutOwner();
                    break;
                case "country":
                    await PutCountry();
                    break;
                case "reviewer":
                    await PutReviewer();
                    break;
                case "category":
                    await PutCategory();
                    break;
                default:
                    Console.WriteLine("Geçersiz istek...");
                    break;
            }
        }

        static async Task DeleteData(string type)
        {
            Console.Write("Silinecek ID: ");
            int id = int.Parse(Console.ReadLine());

            string url = $"https://localhost:7170/api/{type}/{id}";

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            using var client = new HttpClient(handler);
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"{type} Silindi...");
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static async Task PostData(string type)
        {
            switch (type)
            {
                case "food":
                    await PostFood();
                    break;

                case "colour":
                    await PostColour();
                    break;

                case "owner":
                    await PostOwner();
                    break;

                case "country":
                    await PostCountry();
                    break;

                case "pokemon":
                    await PostPokemon();
                    break;

                case "category":
                    await PostCategory();
                    break;

                case "reviewer":
                    await PostReviewer();
                    break;

                case "review":
                    await PostReview();
                    break;

                default:
                    Console.WriteLine("Geçersiz İstek ...");
                    break;
            }
        }
        static async Task PutReview()
        {
            Console.Write("Güncellenecek Review ID: ");
            int reviewId = int.Parse(Console.ReadLine());
            Console.Write("Yeni Title: ");
            string title = Console.ReadLine();
            Console.Write("Yeni Text: ");
            string text = Console.ReadLine();
            Console.Write("Yeni Rating: ");
            int rating = int.Parse(Console.ReadLine());

            var updatedReview = new ReviewDto
            {
                Id = reviewId,
                Title = title,
                Text = text,
                Rating = rating
            };

            string url = $"https://localhost:7170/api/Review/{reviewId}";
            await SendPutRequest(url, updatedReview, "Review");
        }

        static async Task PutReviewer()
        {
            Console.Write("Güncellenecek Reviewer ID: ");
            int reviewerId = int.Parse(Console.ReadLine());
            Console.Write("Yeni firstname: ");
            string firstName = Console.ReadLine();
            Console.Write("Yeni lastname: ");
            string lastName = Console.ReadLine();

            var reviewer = new ReviewerDto { Id = reviewerId, FirstName = firstName, LastName = lastName };
            string url = $"https://localhost:7170/api/Reviewer/{reviewerId}";
            await SendPutRequest(url, reviewer, "Reviewer");
        }

        static async Task PutCategory()
        {
            Console.Write(" Güncellenecek Category ID : ");
            int categoryId = int.Parse(Console.ReadLine());
            Console.Write("Yeni Name: ");
            string name = Console.ReadLine();

            var category = new CategoryDto { Id = categoryId, Name = name };
            string url = $"https://localhost:7170/api/Category/{categoryId}";
            await SendPutRequest(url, category, "Category");
        }

        static async Task PutCountry()
        {
            Console.Write("Güncellenecek Country ID: ");
            int countryID = int.Parse(Console.ReadLine());
            Console.Write("Yeni name: ");
            string name = Console.ReadLine();

            var country = new CountryDto { Id = countryID, Name = name };
            string url = $"https://localhost:7170/api/Country/{countryID}";
            await SendPutRequest(url, country, "Country");
        }

        static async Task PutOwner()
        {
            Console.Write(" Güncellenecek Owner ID: ");
            int ownerId = int.Parse(Console.ReadLine());
            Console.Write("Yeni firstname: ");
            string firstName = Console.ReadLine();
            Console.Write("Yeni lastname: ");
            string lastName = Console.ReadLine();
            Console.Write("Yeni Gym: ");
            string gym = Console.ReadLine();

            var owner = new OwnerDto { Id = ownerId, FirstName = firstName, LastName = lastName, Gym = gym };
            string url = $"https://localhost:7170/api/Owner/{ownerId}";
            await SendPutRequest(url, owner, "Owner");
        }

        static async Task PutFood()
        {
            Console.Write("Güncellenecek Food ID: ");
            int foodID = int.Parse(Console.ReadLine());

            Console.Write("Yeni Name: ");
            string name = Console.ReadLine();

            Console.Write("Pokemon ID: ");
            int pokeId = int.Parse(Console.ReadLine());

            var food = new FoodDto
            {
                Id = foodID,
                Name = name,
                PokemonId = pokeId
            };

            string url = $"https://localhost:7170/api/Food/{foodID}";
            await SendPutRequest(url, food, "Food");
        }

        static async Task PutColour()
        {
            Console.Write("Güncellenecek Colour ID: ");
            int colourID = int.Parse(Console.ReadLine());

            Console.Write("Yeni Name: ");
            string name = Console.ReadLine();

            Console.Write("Pokemon ID: ");
            int pokeId = int.Parse(Console.ReadLine());

            var colour = new ColourDto
            {
                Id = colourID,
                Name = name,
                PokemonId = pokeId
            };

            string url = $"https://localhost:7170/api/Colour/{colourID}";
            await SendPutRequest(url, colour, "Colour");
        }

        static async Task PutPokemon()
        {
            Console.Write("Güncellenecek Pokemon ID");
            int pokeID = int.Parse(Console.ReadLine());
            Console.Write("Yeni Name: ");
            string name = Console.ReadLine();
            Console.Write("Yeni Birthdate (yyyy-MM-dd): ");
            DateTime BirthDate = DateTime.Parse(Console.ReadLine());

            var pokemon = new PokemonDto { Id = pokeID, Name = name, BirthDate = BirthDate };
            string url = $"https://localhost:7170/api/Pokemon/{pokeID}";
            await SendPutRequest(url, pokemon, "Pokemon");
        }

        static async Task SendPutRequest<T>(string url, T data, string entityName)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            using var client = new HttpClient(handler);

            try
            {
                string jsonData = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                    Console.WriteLine($" {entityName} Güncellendi...");
                else
                    Console.WriteLine($"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        static async Task PostCategory()
        {
            Console.Write("Category name: ");
            string name = Console.ReadLine();

            var category = new CategoryDto { Name = name };
            string url = "https://localhost:7170/api/Category";

            await SendPostRequest(url, category, "Category");
        }
        static async Task PostReviewer()
        {
            Console.Write("Reviewer firstname: ");
            string firstName = Console.ReadLine();
            Console.Write("Reviewer lastname: ");
            string lastName = Console.ReadLine();
            var reviewer = new ReviewerDto
            {
                FirstName = firstName,
                LastName = lastName
            };
            string url = "https://localhost:7170/api/Reviewer";
            await SendPostRequest(url, reviewer, "Reviewer");
        }

        static async Task PostReview()
        {
            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.Write("Text: ");
            string text = Console.ReadLine();

            Console.Write("Rating: ");
            int rating = int.Parse(Console.ReadLine());

            Console.Write("Reviewer ID:");
            int reviewerId = int.Parse(Console.ReadLine());

            Console.Write("Pokemon ID:");
            int pokeId = int.Parse(Console.ReadLine());

            var review = new ReviewDto
            {
                Title = title,
                Text = text,
                Rating = rating
            };

            string url = $"https://localhost:7170/api/Review?reviewerId={reviewerId}&pokeId={pokeId}";


            await SendPostRequest(url, review, "Review");

        }

        static async Task SendPostRequest<T>(string url, T data, string entityName)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            using var client = new HttpClient(handler);
            try
            {
                string jsonData = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"{entityName} eklendi.");
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        static async Task PostOwner()
        {

            Console.Write("Owner firstname: ");
            string firstName = Console.ReadLine();

            Console.Write("Owner lastname: ");
            string lastName = Console.ReadLine();

            Console.Write("Gym name: ");
            string gym = Console.ReadLine();

            Console.Write("Country ID: ");
            int countryId = int.Parse(Console.ReadLine());


            var newOwner = new OwnerDto
            {
                FirstName = firstName,
                LastName = lastName,
                Gym = gym
            };

            string url = $"https://localhost:7170/api/Owner?countryId={countryId}";

            await SendPostRequest(url, newOwner, "Owner");
        }


        static async Task PostCountry()
        {
            Console.Write("Country name: ");
            string name = Console.ReadLine();
            var newCountry = new CountryDto
            {
                Name = name
            };
            string url = "https://localhost:7170/api/Country/";
            await SendPostRequest(url, newCountry, "Country");
        }

        static async Task PostPokemon()
        {
            Console.Write("Pokemon name: ");
            string Name = Console.ReadLine();
            Console.Write("Birthdate (yyyy-MM-dd): ");
            DateTime BirthDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Owner ID: ");
            int ownerId = int.Parse(Console.ReadLine());
            var newPokemon = new PokemonDto
            {
                Name = Name,
                BirthDate = BirthDate,
            };

            Console.Write("Category ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            string url = $"https://localhost:7170/api/Pokemon?ownerId={ownerId}&catId={categoryId}";

            await SendPostRequest(url, newPokemon, "Pokemon");
        }


        static async Task PostFood()
        {
            Console.Write("Food name: ");
            string name = Console.ReadLine();

            Console.Write("Pokemon ID: ");
            int pokemonId = int.Parse(Console.ReadLine());

            var newFood = new FoodDto
            {
                Name = name,
                PokemonId = pokemonId
            };

            string url = "https://localhost:7170/api/Food";
            await SendPostRequest(url, newFood, "Food");
        }



        static async Task PostColour()
        {
            Console.Write("Colour name: ");
            string name = Console.ReadLine();

            Console.Write("Pokemon ID: ");
            int pokeId = int.Parse(Console.ReadLine());

            var newColour = new ColourDto
            {
                Name = name,
                PokemonId = pokeId
            };

            string url = "https://localhost:7170/api/Colour";

            await SendPostRequest(url, newColour, "Colour");
        }

    }
}