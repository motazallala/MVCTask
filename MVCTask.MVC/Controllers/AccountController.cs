using Microsoft.AspNetCore.Mvc;
using MVCTask.MVC.Models;
using System.Net;

namespace MVCTask.MVC.Controllers;
public class AccountController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AccountController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }
        // Use HttpClient with cookie handling
        var handler = new HttpClientHandler
        {
            UseCookies = true,
            CookieContainer = new CookieContainer()
        };

        using var client = new HttpClient(handler);
        client.BaseAddress = new Uri("https://localhost:7199"); // API URL

        var response = await client.PostAsJsonAsync("/api/auth/login", request);

        if (response.IsSuccessStatusCode)
        {
            // Extract the cookie from the API response
            if (response.Headers.TryGetValues("Set-Cookie", out var cookieValues))
            {
                // Forward the cookie to the browser
                Response.Headers.Append("Set-Cookie", cookieValues.FirstOrDefault());
            }
            // Cookies from the API are stored in the browser
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Login failed.");
            return View();
        }
    }

    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }
        var handler = new HttpClientHandler
        {
            UseCookies = true,
            CookieContainer = new CookieContainer()
        };
        using var client = new HttpClient(handler);
        client.BaseAddress = new Uri("https://localhost:7199"); // API URL
        var response = await client.PostAsJsonAsync("/api/auth/register", request);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Login");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Registration failed.");
            return View();
        }
    }
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        var authCookie = Request.Cookies["AuthCookie"];
        if (string.IsNullOrEmpty(authCookie))
        {
            return BadRequest("No authentication cookie found.");
        }

        var handler = new HttpClientHandler
        {
            UseCookies = false
        };

        using var client = new HttpClient(handler);
        client.BaseAddress = new Uri("https://localhost:7199");

        var request = new HttpRequestMessage(HttpMethod.Get, "/api/auth/logout");
        request.Headers.Add("Cookie", $"AuthCookie={authCookie}");

        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            Response.Cookies.Delete("AuthCookie");
            return RedirectToAction("Index", "Home");
        }

        return BadRequest("Logout failed.");
    }
}
