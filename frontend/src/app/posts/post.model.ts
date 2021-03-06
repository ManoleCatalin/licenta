import { Interest } from '../interests/interest.model';

export class Post {
  constructor(
    public id: string,
    public title: string,
    public url: string,
    public description: string,
    public thumbnailImgUrl: string,
    public previewImgUrl: string,
    public likesCount: number,
    public likeId: string,
    public favoriteId: string,
    public authorName: string,
    public createdAt: Date,
    public authorId: string,
    public interests: Interest[]
  ) {}
}

export class CreatePostModel {
  constructor(
    public userId: string, public title: string, public sourceUrl: string, public description: string, public interests: string[]
  ) {}
}
