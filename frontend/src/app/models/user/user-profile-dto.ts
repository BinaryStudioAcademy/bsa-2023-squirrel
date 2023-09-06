export interface UserProfileDto {
    userName: string;
    firstName: string;
    lastName: string;
    avatarUrl: string;
    squirrelNotification: boolean;
    emailNotification: boolean;
    isGoogleAuth: boolean;
}
