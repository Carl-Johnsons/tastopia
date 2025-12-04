/* eslint no-unused-vars: "off" */
export enum SettingError {
	NotFound = "SettingError.NotFound",
	InvalidSettingKey = "SettingError.InvalidSettingKey",
	InvalidSettingValue = "SettingError.InvalidSettingValue"
}

export enum UserError {
	NotFound = "UserError.NotFound",
	AlreadyExistUser = "UserError.AlreadyExistUser",
	NullParameters = "UserError.NullParameters",
	FollowFail = "UserError.FollowFail",
	PermissionDenied = "UserError.PermissionDenied",
	UpdateUserFail = "UserError.UpdateUserFail"
}

export enum UserReportError {
	NotFound = "UserReportError.NotFound",
	AddUserReportFail = "UserReportError.AddUserReportFail",
	DeleteUserReportFail = "UserReportError.DeleteUserReportFail",
	UpdateUserReportFail = "UserReportError.UpdateUserReportFail",
	NullParameter = "UserReportError.NullParameter",
	AlreadyMarkComplete = "UserReportError.AlreadyMarkComplete",
	AlreadyPending = "UserReportError.AlreadyPending"
}
