global using Bogus;
global using FluentAssertions;
global using Microsoft.AspNetCore.TestHost;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Mvc.Testing;
global using Testcontainers.MsSql;
global using Respawn;
global using Microsoft.Data.SqlClient;
global using System.Data.Common;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.EntityFrameworkCore;
global using MediatR;

global using InvoiceEngine.API.Database.Entities;
global using InvoiceEngine.API.Enums;
global using InvoiceEngine.API.Database;

global using InvoiceEngine.API.Features.Invoices.Shared;
