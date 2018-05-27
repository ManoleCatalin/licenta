export class Post {
  constructor(
    public title: string,
    public url: string,
    public description: string,
    public thumbnailImgUrl: string,
    public previewImgUrl: string,
    public likesCount: number,
    public liked: boolean,
    public favorite: boolean,
  ) {}
}
