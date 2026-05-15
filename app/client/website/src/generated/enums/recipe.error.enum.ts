/* eslint no-unused-vars: "off" */
export enum CommentError {
  NotFound = "CommentError.NotFound",
  AddCommentFail = "CommentError.AddCommentFail",
  DeleteCommentFail = "CommentError.DeleteCommentFail",
  UpdateCommentFail = "CommentError.UpdateCommentFail",
  AlreadyInactive = "CommentError.AlreadyInactive",
  AlreadyActive = "CommentError.AlreadyActive"
}

export enum RecipeError {
  NotFound = "RecipeError.NotFound",
  AddRecipeFail = "RecipeError.AddRecipeFail",
  DeleteRecipeFail = "RecipeError.DeleteRecipeFail",
  UpdateRecipeFail = "RecipeError.UpdateRecipeFail",
  VoteFail = "RecipeError.VoteFail",
  NullParameter = "RecipeError.NullParameter",
  PermissionDeny = "RecipeError.PermissionDeny",
  AlreadyInactive = "RecipeError.AlreadyInactive",
  AlreadyActive = "RecipeError.AlreadyActive"
}

export enum TagError {
  NotFound = "TagError.NotFound",
  AddTagFail = "TagError.AddTagFail",
  DeleteTagFail = "TagError.DeleteTagFail",
  UpdateTagFail = "TagError.UpdateTagFail",
  NullParameter = "TagError.NullParameter",
  PermissionDeny = "TagError.PermissionDeny",
  AlreadyInactive = "TagError.AlreadyInactive",
  AlreadyExist = "TagError.AlreadyExist",
  ExceedLimitDishTypeTag = "TagError.ExceedLimitDishTypeTag"
}
