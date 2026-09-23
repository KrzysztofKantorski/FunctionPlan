export interface CommentMessage{
    id: number;
    userId: number;
    username: string;
    profilePictureUrl: string | null;
    content: string;
    createdAt: Date;
    parentCommentId: number | null;
    replies: CommentMessage[] | null;
}
