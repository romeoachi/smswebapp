using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using smswebapp.Models;

namespace smswebapp.Tests.Controllers;

[TestFixture]
[TestOf(typeof(StudentsController))]
public class StudentsControllerTest
{
    [Test]
    public void Index_Return_A_ViewResult_WithAListOfStudents_Test()
    {
        var controller = new StudentsController();
        var result = controller.Index();
        Assert.That(result, Is.InstanceOf<ViewResult>());

        var viewResult = (ViewResult)result;
        Assert.That(viewResult.Model, Is.InstanceOf<List<Student>>());
        var model = (List<Student>)viewResult.Model;
        Assert.That(3, Is.EqualTo(model!.Count));
    }

    [Test]
    public void Create_Add_Student_To_List_Test()
    {
        var controller = new StudentsController();
        var student = new Student { Name = "John", Address = "New York" };
        var result = controller.Create(student);

        Assert.That(result, Is.InstanceOf<RedirectToActionResult>(), "Result should be a RedirectToActionResult");
        
        var redirectResult = result as RedirectToActionResult;
        Assert.That(redirectResult!.ActionName, Is.EqualTo("Index"), "Redirect action should be Index");

        var viewResult = controller.Index() as ViewResult;
        Assert.That(viewResult, Is.Not.Null, "Index action should return a ViewResult");
        
        var model = viewResult.Model as List<Student>;
        Assert.That(model, Is.Not.Null, "Model should be a List<Student>");
    }
}