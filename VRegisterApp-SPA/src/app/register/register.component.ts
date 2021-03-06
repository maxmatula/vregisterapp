import { Component, OnInit } from '@angular/core';
import { Register } from '../_models/register';
import { environment } from 'src/environments/environment.prod';
import { HttpClient } from '@angular/common/http';

declare var MediaRecorder: any;


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  constraints = { audio: true };
  model: Register;
  recording = false;
  timer = -6;
  sampleNo: number;
  apiUrl = environment.apiUrl;
  response: string;

  constructor(private http: HttpClient) {
    this.model = new Register();
  }

  ngOnInit() {
  }

  register() {
    this.http.post(`${this.apiUrl}/voice/register`, this.model, {responseType: 'text'}).subscribe(resp => {
      this.response = resp;
    }, error => {
      this.response = 'Unauthorized! Login failed';
    });
  }

  getSample1() {
    this.timer = -6;
    this.sampleNo = 1;
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

  getSample2() {
    this.timer = -6;
    this.sampleNo = 2;
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
          const voiceSample2 = reader.result;
          this.model.voiceSample2 = voiceSample2;
        };
        clearInterval(interval);
      });

    });
  }

  getSample3() {
    this.timer = -6;
    this.sampleNo = 3;
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
          const voiceSample3 = reader.result;
          this.model.voiceSample3 = voiceSample3;
        };
        clearInterval(interval);
      });

    });
  }

}
