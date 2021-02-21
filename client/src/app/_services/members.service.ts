import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PhotoEditorComponent } from '../members/photo-editor/photo-editor.component';
import { Member } from '../_models/member';


@Injectable({
  providedIn: 'root'
})
export class MembersService {
baseUrl = environment.apiUrl;
members: Member[] = [];
  constructor(private  http:HttpClient) { }

  getMembers(){
    if (this.members.length > 0) return of(this.members);
    return this.http.get<Member[]>(this.baseUrl+'users').pipe(
      tap( members => this.members = members)
    );
  }
  getMember(username:string){
    const member = this.members.find( member => member.username === username);
    if (member !== undefined) return of(member);

    return this.http.get<Member>(this.baseUrl+'users/' + username);
  }
  updateMember(member: Member){
    return this.http.put(this.baseUrl+'users', member).pipe(
      tap( () => {
        var index =  this.members.indexOf(member);
        this.members[index] = member;
      })
    );
  }

  setMainPhoto(photoId: number){
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photoId,{})
  }

  deletePhoto(photoId: number){
    return this.http.delete(this.baseUrl + 'users/delete-photo/' + photoId);
  }
}