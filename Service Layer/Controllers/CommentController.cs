using DAL.Models;
using DAL.Repository;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Service_Layer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private readonly ICommentRepo<Comment> _commentRepo;
		public CommentController(ICommentRepo<Comment> commentRepo)
		{
			this._commentRepo = commentRepo;
		}
		[HttpPost]
		[Route("AddComment")]
		[Consumes("application/json")]
		[Produces("application/json")]
		//[Authorize(Roles = "Admin")]
		public IActionResult AddComment([FromBody] Comment comment)
		{
			var newcomment = _commentRepo.AddComment(comment);
			if (newcomment != null)
			{
				return Ok(newcomment);//200
			}
			else
			{
				return BadRequest();
			}
		}
		[HttpDelete]
		[Route("DeleteComment/{id}")]
		[Produces("application/json")]
		public IActionResult DeleteComment(int id)
		{
			var deletedComment = _commentRepo.DeleteComment(id);
			if (deletedComment != null)
			{
				return Ok(deletedComment); // 200
			}
			else
			{
				return NotFound(); // 404
			}
		}
		[HttpPut]
		[Route("UpdateComment")]
		[Consumes("application/json")]
		[Produces("application/json")]
		//[Authorize(Roles = "Admin")]
		public IActionResult UpdateComment([FromBody] Comment comment)
		{
			var updatedComment = _commentRepo.UpdateComment(comment);
			if (updatedComment != null)
			{
				return Ok(updatedComment); // 200
			}
			else
			{
				return BadRequest(); // 400
			}
		}
		[HttpGet]
		[Route("GetCommentById/{id}")]
		[Produces("application/json")]
		public IActionResult GetCommentById(int id)
		{
			var comment = _commentRepo.GetCommentById(id);
			if (comment != null)
			{
				return Ok(comment); // 200
			}
			else
			{
				return NotFound(); // 404
			}
		}
		[HttpGet]
		[Route("GetAllComments")]
		[Produces("application/json")]
		public IActionResult GetAllComment()
		{
			var comments = _commentRepo.GetAllComment();
			if (comments != null && comments.Any())
			{
				return Ok(comments); // 200
			}
			else
			{
				return NoContent(); // 204
			}
		}
	}
}
