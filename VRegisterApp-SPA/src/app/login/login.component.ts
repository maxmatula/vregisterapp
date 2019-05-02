import { Component, OnInit } from '@angular/core';
import { Login } from '../_models/login';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment.prod';

declare var MediaRecorder: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  constraints = { audio: true };
  model: Login;
  context: string;
  apiUrl = environment.apiUrl;
  recording = false;
  timer = -6;
  response: string;

  constructor(private http: HttpClient) {
    this.model = new Login();
  }

  ngOnInit() {
  }

  login() {
    this.http.post(`${this.apiUrl}/voice/login`, this.model, {responseType: 'text'}).subscribe(resp => {
      this.response = resp;
    }, error => {
      this.response = 'Unauthorized! Login failed';
    });
  }

  getSample1() {
    this.timer = -6;
    this.recording = true;
    navigator.mediaDevices.getUserMedia(this.constraints).then(stream => {
      const mediaRecorder = new MediaRecorder(stream);

      const interval = setInterval(() => {
        this.timer++;
        if (this.timer === -1) {
          mediaRecorder.start();
          setTimeout(() => {
            mediaRecorder.stop();
          }, 5000);
        }
      }, 1000);

      const audioChunks = [];

      mediaRecorder.addEventListener('dataavailable', event => {
        audioChunks.push(event.data);
      });

      mediaRecorder.addEventListener('stop', () => {
        this.recording = false;
        const audioBlob = new Blob(audioChunks);
        const reader = new FileReader();
        reader.readAsDataURL(audioBlob);
        reader.onloadend = () => {
          const voiceSample1 = reader.result;
          this.model.voiceSample1 = voiceSample1;
        };
        clearInterval(interval);
      });

    });
  }

  getContext() {
    const body = {
      email: `${this.model.email}`
    };

    this.http.post(`${this.apiUrl}/voice/getcontext`, body, { responseType: 'text' }).subscribe(resp => {
      this.context = resp;
    }, error => {
      console.log(error);
    });
  }

}
