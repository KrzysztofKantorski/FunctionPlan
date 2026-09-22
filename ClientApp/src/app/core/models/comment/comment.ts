export interface Comment{
    id: number;
    userId: number;
    username: string;
    content: string;
    createdAt: Date;
    parentCommentId: number | null;
    replies: Comment[] | null;
}
