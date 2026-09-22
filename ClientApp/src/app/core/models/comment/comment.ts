export interface CommentMessage{
    id: number;
    userId: number;
    username: string;
    content: string;
    createdAt: Date;
    parentCommentId: number | null;
    replies: CommentMessage[] | null;
}
