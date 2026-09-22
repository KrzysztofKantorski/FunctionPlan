export interface Meeting{
    id: number;
    title: string;
    description: string | null;
    scheduledFor: Date;
    organizerId: number;
    organizerName: string; 
}