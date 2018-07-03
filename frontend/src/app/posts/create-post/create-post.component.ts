import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { DataStorageService } from '../../shared/data-storage.service';
import { NgForm } from '@angular/forms';
import { CreatePostModel } from '../post.model';
import { AuthService } from '../../auth/auth.service';
import { Interest } from '../../interests/interest.model';
import { Observable } from 'rxjs-compat/Observable';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent implements OnInit {

  error = false;
  created = false;
  interests: Interest[] = [];
  selectedInterests: Interest[] = [];

  constructor(public activeModal: NgbActiveModal, private dataStorageService: DataStorageService,
    private authService: AuthService, private router: Router) { }

  ngOnInit() {
  }

  onCreatePost(form: NgForm) {
    const title = form.value.title;
    const desc = form.value.desc;
    const url = form.value.url;

    const addedInterests = this.selectedInterests.map(x => x.id);

    const createPostModel = new CreatePostModel(
      this.authService.getCurrentUserId(),
      title, url, desc, addedInterests
    );

    this.dataStorageService.createPost(createPostModel).subscribe(response => {
      this.created = true;
    }, error => {
      this.error = true;
    });
  }

  setInterest($event) {
    const interests = this.interests.filter(interest => {
      return interest.name === $event.item;
   });

    if (interests.length === 1) {
      this.selectedInterests.push(interests[0]);
    }
  }

  displayPostsOfInterest(interest: string) {
    this.activeModal.close('done');
    this.router.navigate(['/posts', 'interest', interest]);
  }

  removeInterestFromSelected(interest: string) {
    this.selectedInterests = this.selectedInterests.filter(x => x.id !== interest);
  }

  search = (text$: Observable<string>) => {
    this.dataStorageService
    .getAvailableInterests(1, 400).subscribe(ints => {
      this.interests = ints;
    }
    );

    return text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      map(term => {
        return term.length < 2
          ? []
          : this.interests.filter(v => v.name.toLowerCase().indexOf(term.toLowerCase()) > -1)
          .map(v => v.name).map(v => {
            return v;
          })
          .slice(0, 10);
        }
      ));
  }
}
