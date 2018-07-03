import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { DataStorageService } from '../../shared/data-storage.service';
import { Interest } from '../interest.model';
import { RouterModule, Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-display-interests',
  templateUrl: './display-interests.component.html',
  styleUrls: ['./display-interests.component.css']
})
export class DisplayInterestsComponent implements OnInit {
  @Input() name;

  public searchedInterest: Interest;
  public imgThumbnailSelected = '';
  public displayInterestName = '';
  public activeInterests: Interest[] = [];
  public interests: Interest[] = [];

  constructor(
    public activeModal: NgbActiveModal,
    private dataStorageService: DataStorageService,
    private router: Router,
    private httpClient: HttpClient
  ) {}

  ngOnInit() {
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
    const getImgSearchGoogle =
    'https://www.googleapis.com/customsearch/v1?key=' +
    environment.customGoogleSearchApiKey +
    '&cx=010971677492822427252:8_y1rtvjy0u&num=1&start=1&imgSize=medium&searchType=image&q=';

    const client = this.httpClient;

    this.dataStorageService.getAvailableInterests(1, 400).subscribe(
      ints => {
        this.interests = ints;
      }
    );

    return text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      map(term => {
        return term.length < 2
          ? []
          : this.interests
              .filter(v => v.name.toLowerCase().indexOf(term.toLowerCase()) > -1)
              .map(v => v.name)
              .map(v => {
                return v;
              })
              .slice(0, 10);
      })
    );
  }

  addInterest() {

      this.dataStorageService.addActiveInterest(this.searchedInterest.id).subscribe(result => {

      this.dataStorageService.getActiveInterests().subscribe(result2 => {
        this.activeInterests = result2;
      });
    });
  }

  removeInterest(interest: string) {
    this.dataStorageService.removeActiveInterest(interest).subscribe(result => {
      this.dataStorageService.getActiveInterests().subscribe(result2 => {
        this.activeInterests = result2;
      });
    });

  }

  displayPostsOfInterest(interest: string) {
    this.activeModal.close('done');
    this.router.navigate(['/posts', 'interest', interest]);
  }

  closeModal() {
    this.activeModal.close('Close click');
    window.location.reload();
  }
}
