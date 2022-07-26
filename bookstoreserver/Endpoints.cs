﻿using bookstoreserver.Data;
using Microsoft.OpenApi.Models;

namespace bookstoreserver
{
    public class Endpoints
    {
        public class UserEndpoints
        {
            public static void Get(WebApplication app)
            {
                app.MapGet("/get-user/{userId}", async (int userId) =>
                {
                    User foundUser = await UsersRepository.GetUserByIdAsync(userId);
                    if (foundUser != null)
                    {
                        return Results.Ok(foundUser);
                    }
                    else
                    {
                        return Results.NotFound();
                    }
                }).WithTags("User Endpoints");

            }
            public static void Login(WebApplication app)
            {
                app.MapPost("/login", async (User user) =>
                {
                    User foundUser = await UsersRepository.LoginAsync(user);
                    if (foundUser is not null)
                    {
                        return Results.Ok(foundUser);
                    }
                    else
                    {
                        return Results.Conflict(CustomResponses.LoginError);
                    }
                }).WithTags("User Endpoints");

            }
            public static void Signup(WebApplication app)
            {
                app.MapPost("/signup", async (User userToCreate) =>
                {
                    bool createSuccesfull = await UsersRepository.SignupAsync(userToCreate);
                    if (createSuccesfull)
                    {
                        return Results.Ok(userToCreate);
                    }
                    else
                    {
                        return Results.Conflict(CustomResponses.UserFound);
                    }
                }).WithTags("User Endpoints");

            }
        }
        public class BookEndpoints
        {
            public static void All(WebApplication app)
            {
                app.MapGet("/get-all-books", async () => await BooksRepository.GetBooksAsync()).WithTags("Book Endpoints");
            }
            public static void Get(WebApplication app)
            {
                app.MapGet("/get-book/{bookId}", async (int bookId) =>
                {
                    try
                    {
                        Book bookToReturn = await BooksRepository.GetBookByIdAsync(bookId);
                        if (bookToReturn != null)
                        {
                            return Results.Ok(bookToReturn);
                        }
                        else
                        {
                            return Results.NotFound();
                        }
                    }
                    catch (Exception ex)
                    {
                        return Results.NotFound();
                    }

                }).WithTags("Book Endpoints");
            }
            public static void Create(WebApplication app)
            {
                app.MapPost("/create-book", async (Book bookToCreate) =>
                {
                    bool createSuccesfull = await BooksRepository.CreateBookAsync(bookToCreate);
                    if (createSuccesfull)
                    {
                        return Results.Ok(bookToCreate);
                    }
                    else
                    {
                        return Results.Conflict(CustomResponses.OnCreate);
                    }
                }).WithTags("Book Endpoints");

            }
            public static void Update(WebApplication app)
            {
                app.MapPut("/update-book", async (Book bookToUpdate) =>
                {
                    bool updateSuccesfull = await BooksRepository.UpdateBookAsync(bookToUpdate);
                    if (updateSuccesfull)
                    {
                        return Results.Ok(bookToUpdate);
                    }
                    else
                    {
                        return Results.Conflict(CustomResponses.OnUpdate);
                    }
                }).WithTags("Book Endpoints");

            }
            public static void Delete(WebApplication app)
            {
                app.MapDelete("/delete-book/{bookId}", async (int bookId) =>
                {
                    Book bookToDelete = await BooksRepository.GetBookByIdAsync(bookId);
                    bool deleteSuccesfull = await BooksRepository.DeleteBookAsync(bookId);
                    if (deleteSuccesfull)
                    {
                        return Results.Ok(bookToDelete);
                    }
                    else
                    {
                        return Results.Conflict(CustomResponses.OnDelete);
                    }
                }).WithTags("Book Endpoints");

            }
        }

    }
}
