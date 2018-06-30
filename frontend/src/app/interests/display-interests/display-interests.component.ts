import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { DataStorageService } from '../../shared/data-storage.service';
import { Interest } from '../interest.model';
import { RouterModule, Router } from '@angular/router';

@Component({
  selector: 'app-display-interests',
  templateUrl: './display-interests.component.html',
  styleUrls: ['./display-interests.component.css']
})
export class DisplayInterestsComponent implements OnInit  {
  @Input() name;

  public searchedInterest: Interest;
  public imgThumbnailSelected = '';
  public displayInterestName = '';
  public activeInterests: Interest[] = [];
  public interests: Interest[] = [];

  constructor(public activeModal: NgbActiveModal, private dataStorageService: DataStorageService, private router: Router) {
    console.log('ctor');
    // this.activeInterests = dataStorageService.getActiveInterests(1, 5);
  }

  ngOnInit() {
      console.log('on init');
      this.dataStorageService.getActiveInterests().subscribe(result => {
          this.activeInterests = result;
      });
  }

  setPhoto($event) {

    const interests = this.interests.filter(interest => {
      return interest.name === $event.item;
   });

    if (interests.length === 1) {
      this.imgThumbnailSelected = interests[0].thumbnailImgUrl;

      this.searchedInterest = interests[0];

      this.displayInterestName = this.searchedInterest.name;
    }
  }

  search = (text$: Observable<string>) => {

    this.dataStorageService
    .getAvailableInterests(1, 400).subscribe(ints => {
      console.log('ints: ', ints);
      this.interests = ints;
    },
    error => {
      console.log('error: ' + error);
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
            console.log(v);
            return v;
          })
          .slice(0, 10);
        }
      ));
  }

  addInterest() {
    console.log(this.searchedInterest);
    this.dataStorageService.addActiveInterest(this.searchedInterest.id).subscribe(result => {
      console.log('added interest successfully');
      this.dataStorageService.getActiveInterests().subscribe(result2 => {
        this.activeInterests = result2;
    });
    });

    console.log(this.activeInterests);
  }

  removeInterest(interest: string) {
    this.dataStorageService.removeActiveInterest(interest).subscribe(result => {
      console.log('removed interest successfully');
      this.dataStorageService.getActiveInterests().subscribe(result2 => {
        this.activeInterests = result2;
    });
    });

    console.log(this.activeInterests);
  }

  displayPostsOfInterest(interest: string) {
    console.log('displayPostsOfInterest ' + interest);
    this.activeModal.close('done');
    this.router.navigate(['/posts', 'interest', interest]);
  }


}
