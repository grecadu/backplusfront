import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class ListingsService {
  private apiUrl = 'src/assets/listings.json';


  constructor(private http: HttpClient) { }

  getListings(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
}

export interface ListingModel {
  Id: number;
  DateListed: string;
  Title: string;
  Description: string;
  "Sale Price": number;
  ThumbnailURL: string;
  PictureURL: string;
  Location: string;
  Sqft: number;
  Bedrooms: number;
  Bathrooms: number;
  Parking: number;
  YearBuilt: number;
}
