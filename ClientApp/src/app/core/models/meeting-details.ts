import { Organizer } from "./organizer";

export interface MeetingDetails
{
    id: number;
    title: string;
    scheduledFor: Date;
    latitude: number;
    longitude: number;
    organizer: Organizer;
}
