global using NUnit.Framework;
global using FamilyTests;
global using Familynk;
global using Familynk.Controllers;
global using Familynk.Data;
global using Familynk.Models.Messages;
global using Familynk.Interfaces;
global using Familynk.Migrations;
global using Familynk.Models;
global using Familynk.Util;
global using Familynk.ViewModels;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Moq;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Security.Claims;
global using System.Threading.Tasks;
global using AspNetCoreHero;
global using AspNetCoreHero.ToastNotification;
global using AspNetCoreHero.ToastNotification.Abstractions;
global using AspNetCoreHero.ToastNotification.Notyf;
global using AspNetCoreHero.ToastNotification.Containers;
global using AspNetCoreHero.ToastNotification.Extensions;
global using AspNetCoreHero.ToastNotification.Helpers;


global using Microsoft.IdentityModel.Tokens;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.EntityFrameworkCore.InMemory;

global using System.Diagnostics;
global using System.Resources;
global using System.Collections;
global using System.ComponentModel;
global using Microsoft.AspNetCore.Mvc.ModelBinding;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;


global using IServiceProvider = System.IServiceProvider;

/*
TODO Notifications
TODO Tag System
TODO words on new family input
TODO Events show up on calendar
TODO Site Admin are
TODO Magnetic message
TODO Family History
TODO front porch
    - if a user is not registered to a family, they may only see the familie front porch, and leave a message.
    - (Maybe) a non-family member may request to be in a family.
TODO Family Tree
TODO Invites:
    - A family member can send an invitation to either a registered users DM's or an unregistered users email.
    - A user cannot join a family without an invitation code.
TODO Users Bedroom
    - upload and set avarars
    - choose bubble colors
    - color schemes?
*/