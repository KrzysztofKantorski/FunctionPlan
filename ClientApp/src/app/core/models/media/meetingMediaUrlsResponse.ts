export interface MeetingMediaUrlsResponse
{
    fileName: string;
    description?: string | null;
    createdAt: Date;
    uploaderId: number;
    uploaderName: string;
    uploaderAvatarId?: string | null;
}
